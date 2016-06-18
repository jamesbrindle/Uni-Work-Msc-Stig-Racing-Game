//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Server Admin Commands
//-----------------------------------------------------------------------------

function SAD(%password)
{
   if (%password !$= "")
      commandToServer('SAD', %password);
}

function SADSetPassword(%password)
{
   commandToServer('SADSetPassword', %password);
}


//----------------------------------------------------------------------------
// Misc server commands
//----------------------------------------------------------------------------

function clientCmdSyncClock(%time)
{
   // Time update from the server, this is only sent at the start of a mission
   // or when a client joins a game in progress.
}

//----------------------------------------------------------------------------
// Get speed - get the current control object speed and sets corresponding
// bitmaps on screen to act as the speedomenter
//----------------------------------------------------------------------------

   function clientCmdGetSpeed()
 {
       %texPath = "racinggame/client/ui/images/speedHUD/";
       %vel = getControlObjectSpeed();
       %cVel = mFloor(%vel * 3.4);

       if(%cVel <= 9) //if speed under 9, only change first digit
       {
           digit_1.setBitmap(%texPath @ "0");
           digit_2.setBitmap(%texPath @ "0");
           digit_3.setBitmap(%texPath @ %cVel);
       }

       if(%cVel >= 10) //if speed over 10, change second digit also
       {
           %speed1 = getSubStr(%cVel, 0, 1);
           %speed2 = getSubStr(%cVel, 1, 1);
           digit_1.setBitmap(%texPath @ "0");
           digit_2.setBitmap(%texPath @ %speed1);
           digit_3.setBitmap(%texPath @ %speed2);
       }

       if(%cVel >= 100) //if speed over 100 change 3rd digit also
       {
           %speed3 = getSubStr(%cVel, 2, 1);
           digit_1.setBitmap(%texPath @ %speed1);
           digit_2.setBitmap(%texPath @ %speed2);
           digit_3.setBitmap(%texPath @ %speed3);
      }
      $speedHudSchedule = schedule(200, 0, "clientCmdGetSpeed");
   }


