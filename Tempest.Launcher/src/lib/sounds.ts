const sounds = defineSounds({
	clickImpact: [new Audio("/audio/click-impact.ogg"), 0.1],
	clickSwitch: [new Audio("/audio/click-switch.ogg"), 0.1],
	clickTick: [new Audio("/audio/click-tick.ogg"), 0.1],
	hover: [new Audio("/audio/hover.ogg"), 0.003],
	play: [new Audio("/audio/play.ogg"), 0.1],
});

function defineSounds<T extends string>(sounds: Record<T, [HTMLAudioElement, number]>) {
	return sounds as Record<T, [HTMLAudioElement, number]>;
}

export const playSound = (soundName: keyof typeof sounds) => {
	const value = sounds[soundName];
	if (value) {
		const [sound, volume] = value;

		sound.volume = volume;
		sound.currentTime = 0;

		sound.play().catch((error) => {
			console.error(`Error playing sound ${soundName}:`, error);
		});
	} else {
		console.warn(`Sound ${soundName} not found.`);
	}
};
