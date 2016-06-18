//-----------------------------------------------------------------------------
// Torque Game Engine
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

datablock TriggerData(CheckPointTrigger)
{
   // The period is value is used to control how often the console
   // onTriggerTick callback is called while there are any objects
   // in the trigger.  The default value is 100 MS.
   tickPeriodMS = 2000;
};

//-----------------------------------------------------------------------------

function CheckPointTrigger::onEnterTrigger(%this,%trigger,%obj)
{
	Parent::onEnterTrigger(%this,%trigger,%obj);

       	if(%obj.client.nextCheck == %trigger.checkpoint)
	{
		if(%trigger.isLast)
		{
			// Player has completed a lap.
			%obj.client.lap++;

			if(%obj.client.lap >= $Game::Laps)
			{
				// Increase his score by 1.
				%obj.client.incScore(1);
            	// End the game
             	cycleGame();			
			}
			else { //all laps complete
				%obj.client.nextCheck = 1;

                                		
				commandToClient(%obj.client, 'IncreaseLapCounter');//increase lap counter
                                headsUpTextPrint(%obj.client, "Lap Complete!", 3); //display lap complete message
			}
		}
		else {
			// Continue to the next one.
			%obj.client.nextCheck++;

                       headsUpTextPrint(%obj.client, "Checkpoint Passed!", 3); //display checkpoint passed message
                 }
	}
	else {
              
            if(%trigger.isCheat) { //if checkpoint is an 'is cheating checkpoint'
                    headsUpTextPrint(%obj.client, "Stay On The Track!!!", 3); //display is cheating message
                }
            else { //if not an 'is cheating checkpoint'
            headsUpTextPrint(%obj.client, "Wrong Way!", 3); //display a going the wrong way message
            }
                                
       }
}
