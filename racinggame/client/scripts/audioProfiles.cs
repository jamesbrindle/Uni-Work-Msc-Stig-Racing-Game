//-----------------------------------------------------------------------------
// Torque Game Engine
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

// Channel assignments (channel 0 is unused in-game).

$GuiAudioType     = 1;
$SimAudioType     = 2;
$MessageAudioType = 3;

new AudioDescription(AudioGui)
{
   volume   = 1.0;
   isLooping= false;
   is3D     = false;
   type     = $GuiAudioType;
};

new AudioDescription(inGameMusicDesc)
{
   volume   = 0.93;
   isLooping= false;
   is3D     = false;
   type     = $GuiAudioType;
};

new AudioDescription(revDesc)
{
   volume   = 0.7;
   isLooping= false;
   is3D     = false;
   type     = $GuiAudioType;
};


new AudioDescription(guiThemeDesc)
{
   volume   = 0.89;
   isLooping= true;
   is3D     = false;
   type     = $GuiAudioType;
};

new AudioDescription(engineStartDesc)
{
   volume   = 0.93;
   isLooping= false;
   is3D     = false;
   type     = $GuiAudioType;
};

new AudioDescription(startupSoundDesc)
{
   volume   = 0.85;
   isLooping= false;
   is3D     = false;
   type     = $GuiAudioType;
};

new AudioDescription(AudioMessage)
{
   volume   = 1.0;
   isLooping= false;
   is3D     = false;
   type     = $MessageAudioType;
};

//sound to play when moving over particular buttons
new AudioProfile(AudioButtonOver)
{
   filename = "~/data/sound/buttonOver.wav";
   description = "AudioGui";
	preload = true;
};

//sound to play when game countdown begins before racing
new AudioProfile(AudioCountBeep)
{
	filename = "~/data/sound/beep1.wav";
	description = "AudioGui";
	preload = true;
};

//sound to play when countdown has finished to begin racing
new AudioProfile(AudioGoBeep)
{
	filename = "~/data/sound/beep2.wav";
	description = "AudioGui";
	preload = true;
};

//sound to play when a checkpoint has passed when racing
new AudioProfile(CheckpointBeep)
{
	filename = "~/data/sound/beep2.wav";
	description = "AudioGui";
	preload = true;
};

//sound to play at torque loading screen
new AudioProfile(EngineSound1)
{
	filename = "~/data/sound/engineSound1.wav";
	description = "startupSoundDesc";
	preload = true;
};

//theme music to play while on main menu
new AudioProfile(guiThemeTune)
{
	filename = "~/data/sound/guiTheme2.ogg";
	description = "guiThemeDesc";
	preload = true;
};

//sound to play when loading a race
new AudioProfile(revSound)
{
	filename = "~/data/sound/revSound.wav";
	description = "revDesc";
	preload = true;
};

//sound to play when moving over certain button while on main menu
new AudioProfile(engineStart)
{
	filename = "~/data/sound/engineStart.wav";
	description = "engineStartDesc";
	preload = true;
};

