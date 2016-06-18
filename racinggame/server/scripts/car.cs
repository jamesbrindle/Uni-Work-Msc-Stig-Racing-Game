//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------

// Information extacted from the shape.
//
// Wheel Sequences
//    spring#        Wheel spring motion: time 0 = wheel fully extended,
//                   the hub must be displaced, but not directly animated
//                   as it will be rotated in code.
// Other Sequences
//    steering       Wheel steering: time 0 = full right, 0.5 = center
//    breakLight     Break light, time 0 = off, 1 = breaking
//
// Wheel Nodes
//    hub#           Wheel hub, the hub must be in it's upper position
//                   from which the springs are mounted.
//
// The steering and animation sequences are optional.
// The center of the shape acts as the center of mass for the car.

//-----------------------------------------------------------------------------

datablock AudioProfile(CarEngineSound)
{
    filename = "~/data/sound/engine_idle.wav";
    description = AudioClosestLooping3d;
    preload = true;

};
datablock AudioProfile(CarEngineAccSound)
{
    filename = "~/data/sound/engine_acc.wav";
    description = AudioClosestLooping3d;
    preload = true;
};
datablock AudioProfile(CarScrubSound)
{
    filename = "~/data/sound/scrub.wav";
    description = AudioClose3d;
    preload = true;
};
datablock AudioProfile(CarCrashSound)
{
    filename = "~/data/sound/crash.wav";
    description = AudioClose3d;
    preload = true;
};
datablock AudioProfile(CarSkidSound)
{
    filename = "~/data/sound/skid.wav";
    description = AudioClose3d;
    preload = true;
};

datablock ParticleData(TireParticle)
{
   textureName          = "~/data/shapes/buggy/dustParticle";
   dragCoefficient      = 2.0;
   gravityCoefficient   = -0.1;
   inheritedVelFactor   = 0.1;
   constantAcceleration = 0.0;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 0;
   colors[0]     = "0.46 0.36 0.26 1.0";
   colors[1]     = "0.46 0.46 0.36 0.0";
   sizes[0]      = 0.50;
   sizes[1]      = 1.0;
};

datablock ParticleEmitterData(TireEmitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 1;
   ejectionVelocity = 1;
   velocityVariance = 1.0;
   ejectionOffset   = 0.2;
   thetaMin         = 5;
   thetaMax         = 20;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "TireParticle";
};


//----------------------------------------------------------------------------

datablock WheeledVehicleTire(DefaultCarTire)
{
   // Tires act as springs and generate lateral and longitudinal
   // forces to move the vehicle. These distortion/spring forces
   // are what convert wheel angular velocity into forces that
   // act on the rigid body.
   shapeFile = "~/data/shapes/gto/ferrari_wheel.dts";
   staticFriction = 2;
   kineticFriction = 2.4;

   // Spring that generates lateral tire forces
   lateralForce = 700000;
   lateralDamping = 300;
   lateralRelaxation = 1;

   // Spring that generates longitudinal tire forces
   longitudinalForce = 12000;
   longitudinalDamping = 100;
   longitudinalRelaxation = 1;
};

datablock WheeledVehicleSpring(DefaultCarSpring)
{
   // Wheel suspension properties
   length = 0.35;             // Suspension travel
   force = 3000;              // Spring force
   damping = 600;             // Spring damping
   antiSwayForce = 4;         // Lateral anti-sway force
};

datablock WheeledVehicleData(DefaultCar)
{
   category = "Vehicles";
   shapeFile = "~/data/shapes/gto/ferrari_gto.dts";
   emap = true;

   maxDamage = 1.0;
   destroyedLevel = 0.5;

   maxSteeringAngle = 0.40;  // Maximum steering angle, should match animation
   tireEmitter = TireEmitter; // All the tires use the same dust emitter

   // 3rd person camera settings
   cameraRoll = true;         // Roll the camera with the vehicle
   cameraMaxDist = 5;         // Far distance from vehicle
   cameraOffset = 1.5;        // Vertical offset from camera mount point
   cameraLag = 0.04;           // Velocity lag of camera
   cameraDecay = 0.75;        // Decay per sec. rate of velocity lag

   // Rigid Body
   mass = 200;
   massCenter = "0.1 -0.3 0.1";    // Center of mass for rigid body
   massBox = "0 0 0";         // Size of box used for moment of inertia,
                              // if zero it defaults to object bounding box
   drag = 0.6;                // Drag coefficient
   bodyFriction = 0.6;
   bodyRestitution = 0.04;
   minImpactSpeed = 5;        // Impacts over this invoke the script callback
   softImpactSpeed = 5;       // Play SoftImpact Sound
   hardImpactSpeed = 30;      // Play HardImpact Sound
   integration = 4;           // Physics integration: TickSec/Rate
   collisionTol = 0.1;        // Collision distance tolerance
   contactTol = 0.1;          // Contact velocity tolerance

   // Engine
   engineTorque = 4000;       // Engine power
   engineBrake = 800;         // Braking when throttle is 0
   brakeTorque = 4000;        // When brakes are applied
   maxWheelSpeed = 55;        // Engine scale by current speed / max speed

   // Energy
   maxEnergy = 100;
   jetForce = 3000;
   minJetEnergy = 30;
   jetEnergyDrain = 2;

   // Sounds
   jetSound = CarEngineAccSound;
   engineSound = CarEngineAccSound;
   squealSound = CarSkidSound;
   softImpactSound = CarCrashSound;
   hardImpactSound = CarCrashSound;
   wheelImpactSound = CarScrubSound;

//   explosion = VehicleExplosion;

};


//-----------------------------------------------------------------------------

function WheeledVehicleData::create(%block)
{
   %obj = new WheeledVehicle() {
      dataBlock = %block;
   };
   return(%obj);
}

//-----------------------------------------------------------------------------

function WheeledVehicleData::onAdd(%this,%obj)
{
   // Setup the car with some defaults tires & springs
   for (%i = %obj.getWheelCount() - 1; %i >= 0; %i--) {
      %obj.setWheelTire(%i,DefaultCarTire);
      %obj.setWheelSpring(%i,DefaultCarSpring);
      %obj.setWheelPowered(%i,false);

   commandToClient(%obj.client, 'getSpeed');
   }

   // Steer front tires
   %obj.setWheelSteering(0,1);
   %obj.setWheelSteering(1,1);

   // Only power the two rear wheels...
   %obj.setWheelPowered(0,true);
   %obj.setWheelPowered(1,true);
   %obj.setWheelPowered(2,true);
   %obj.setWheelPowered(3,true);
}

function WheeledVehicleData::onCollision(%this,%obj,%col,%vec,%speed)
{
   // Collision with other objects, including items
}   

