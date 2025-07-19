const sounds = defineSounds({
	clickImpact: [new Audio("/audio/click-impact.mp3"), 0.1],
	clickSwitch: [new Audio("/audio/click-switch.mp3"), 0.1],
	clickTick: [new Audio("/audio/click-tick.mp3"), 0.1],
	hover: [new Audio("/audio/hover.mp3"), 0.003],
});

function defineSounds<T extends string>(sounds: Record<T, [HTMLAudioElement, number]>) {
	return sounds as Record<T, [HTMLAudioElement, number]>;
}

export function playSound(soundName: keyof typeof sounds) {
	const value = sounds[soundName];
	if (value) {
		const [sound, volume] = value;

		sound.currentTime = 0;
		sound.volume = volume;

		sound.play().catch((error) => {
			console.error(`Error playing sound ${soundName}:`, error);
		});
	} else {
		console.warn(`Sound ${soundName} not found.`);
	}
}
