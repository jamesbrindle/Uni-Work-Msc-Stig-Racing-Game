//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// PlayGui is the main TSControl through which the game is viewed.
// The PlayGui also contains the hud controls.
//-----------------------------------------------------------------------------

function PlayGui::onWake(%this)
{
   // Turn off any shell sounds...


   $enableDirectInput = "1";
   activateDirectInput();
   enableJoystick();

   // Message hud dialog
   Canvas.pushDialog( MainChatHud );
   chatHud.attach(HudMessageVector);

   // just update the action map here
   moveMap.push();
   
   // hack city - these controls are floating around and need to be clamped
   schedule(0, 0, "refreshCenterTextCtrl");
   schedule(0, 0, "refreshBottomTextCtrl");

}

function PlayGui::onSleep(%this)
{
   Canvas.popDialog( MainChatHud  );

   
   // pop the keymaps
   moveMap.pop();
}

//-----------------------------------------------------------------------------
//updates the on screen message text showing the lap counter
function PlayGui::updateLapCounter(%this)
{
	LapCounter.setText("Lap" SPC %this.lap SPC "/" SPC %this.maxLaps);
}

//sets the maximum number of laps, also displays that on the screen
function clientCmdSetMaxLaps(%laps)
{
	// Reset the current lap to 1 and set the max laps.
	PlayGui.lap = 1;
	PlayGui.maxLaps = %laps;
	PlayGui.updateLapCounter();
}

//increases lap counter when car crosses specified checkpoint
function clientCmdIncreaseLapCounter()
{
	// Increase the lap.
	PlayGui.lap++;
	PlayGui.updateLapCounter();
}


//-----------------------------------------------------------------------------

function refreshBottomTextCtrl()
{
   BottomPrintText.position = "0 0";
}

function refreshCenterTextCtrl()
{
   CenterPrintText.position = "0 0";
}

   