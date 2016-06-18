//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//----------------------------------------------------------------------------
// Game start / end events sent from the server
//----------------------------------------------------------------------------

//function to add a countdown before the actual race begins and display countdown lights
//on screen
function clientCmdSetCounter(%count)
{

	if(%count == 3)
		counter.setVisible(true);

        //changes the bitmap image accordingly
	counter.setBitmap("racinggame/client/ui/images/countdown/" @ %count @ ".png");        

	alxPlay(AudioCountBeep);
}

//removes last coundown image, plays a go sound and starts the race timer
function clientCmdGameStart(%seq)
{

        counter.schedule(1000, setVisible, false);
        alxPlay(AudioGoBeep);
        clientCmdStartTimer(0);
}

//start the timer
function clientCmdStartTimer(%time) {
    echo("timer started");
    schedule(100, 0, clientCmdTickTimer, %time, %time, %time);
}

//start the timer increment
function clientCmdTickTimer(%time1, %time2, %time3) {

   //time3 = 100th of a second
   //time2 = 1 second
   //time1 = minute
    timer.setText("Time: " @ %time1 @ ":" @ %time2 @ ":" @ %time3);

    //used to keep the time in the correct time format i.e. mm:ss:hs
    if(%time3==100) {
        %time3=0;
        %time2++;
        if(%time2==60) {
            %time2=0;
            %time1++;
        }
    }
    else {
        %time3++;
    }

    schedule(10, 0, ClientCmdTickTimer, %time1, %time2, %time3); //update timer every 100th of a second
}


function clientCmdGameEnd(%seq)
{
   // Stop local activity... the game will be destroyed on the server
   alxStopAll();


   // Copy the current scores from the player list into the
   // end game gui (bit of a hack for now).
   EndGameGuiList.clear();
   for (%i = 0; %i < PlayerListGuiList.rowCount(); %i++) {
      %text = PlayerListGuiList.getRowText(%i);
      %id = PlayerListGuiList.getRowId(%i);
      EndGameGuiList.addRow(%id,%text);
   }
   EndGameGuiList.sortNumerical(1,false);

   // Display the end-game screen
   Canvas.setContent(EndGameGui);
}
