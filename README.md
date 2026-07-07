# About Aphasic

Aphasic is a lightweight overlay I made with WPF using the .NET framework. It lets me quickly see whether my microphone is muted when I'm using a mute hotkey instead of muting directly in an application like VoiceMod or Discord.

I do plan to update this maybe with a few options like a slider for size and opacity.

## Usage

Press `]` to toggle the mute indicator.

## Syncing with Your Microphone

Aphasic tracks the state of your microphone based on each time you press the hotkey—it does **not** detect your microphone's actual mute state.

Before launching Aphasic, make sure your microphone is **unmuted** in VoiceMod (or whichever application you're using). Then start Aphasic and make sure that the indicator matches your microphone's current state. From then on, use the `]` hotkey to keep the overlay synchronized with your microphone.
