//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

$centerPrintActive = 0;
$bottomPrintActive = 0;

// Selectable window sizes
$CenterPrintSizes[1] = 20;
$CenterPrintSizes[2] = 36;
$CenterPrintSizes[3] = 56;

// time is specified in seconds
//Prints various checkpoint message to centre of screen and play a sound
function clientCmdHeadsUpTextPrint(%message, %time)
{
    if ($HeadsUpTextActive) {
      if(HeadsUpText.removePrint !$= "")
         cancel(HeadsUpText.removePrint); //removes message
   }
   else {
      HeadsUpText.setVisible(true);
      $HeadsUpTextTextActive = 1;
   }

   alxPlay(checkPointBeep);
   HeadsUpText.setText("<just:center>" @ %message);
   HeadsUpText.setVisible(true);

 if (%time > 0)
      HeadsUpText.removePrint = schedule( ( %time * 1000 ), 0, "clientCmdClearHeadsUpTextPrint" );
}

// time is specified in seconds
function clientCmdCenterPrint( %message, %time, %size )
{
   // if centerprint already visible, reset text and time.
   if ($centerPrintActive) {   
      if (centerPrintDlg.removePrint !$= "")
         cancel(centerPrintDlg.removePrint);
   }
   else {
      CenterPrintDlg.visible = 1;
      $centerPrintActive = 1;
   }

   CenterPrintText.setText( "<just:center>" @ %message );
   CenterPrintDlg.extent = firstWord(CenterPrintDlg.extent) @ " " @ $CenterPrintSizes[%size];
   
   if (%time > 0)
      centerPrintDlg.removePrint = schedule( ( %time * 1000 ), 0, "clientCmdClearCenterPrint" );
}

// time is specified in seconds
function clientCmdBottomPrint( %message, %time, %size )
{
   // if bottomprint already visible, reset text and time.
   if ($bottomPrintActive) {   
      if( bottomPrintDlg.removePrint !$= "")
         cancel(bottomPrintDlg.removePrint);
   }
   else {
      bottomPrintDlg.setVisible(true);
      $bottomPrintActive = 1;
   }
   
   bottomPrintText.setText( "<just:center>" @ %message );
   bottomPrintDlg.extent = firstWord(bottomPrintDlg.extent) @ " " @ $CenterPrintSizes[%size];

   if (%time > 0)
      bottomPrintDlg.removePrint = schedule( ( %time * 1000 ), 0, "clientCmdClearbottomPrint" );
}

function BottomPrintText::onResize(%this, %width, %height)
{
   %this.position = "0 0";
}

function CenterPrintText::onResize(%this, %width, %height)
{
   %this.position = "0 0";
}

//-------------------------------------------------------------------------------------------------------
//clear text from screen functions
function clientCmdClearHeadsUpTextPrint()
{
   $HeadsUpTextActive = 0;
   HeadsUpText.visible = 0;
   HeadsUpText.removePrint = "";
}

function clientCmdClearCenterPrint()
{
   $centerPrintActive = 0;
   CenterPrintDlg.visible = 0;
   CenterPrintDlg.removePrint = "";
}

function clientCmdClearBottomPrint()
{
   $bottomPrintActive = 0;
   BottomPrintDlg.visible = 0;
   BottomPrintDlg.removePrint = "";
}
