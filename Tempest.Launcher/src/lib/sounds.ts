function createAudio(src: string, volume: number) {
	const audio = new Audio(src);
	audio.volume = volume;
	return audio;
}

export const sounds = {
	clickImpact: createAudio("/audio/click-impact.ogg", 0.1),
	clickSwitch: createAudio("/audio/click-switch.ogg", 0.1),
	clickTick: createAudio("/audio/click-tick.ogg", 0.1),
	denied: createAudio("/audio/denied.ogg", 0.1),
	hover: createAudio("/audio/hover.ogg", 0.003)
};

export const playSound = (soundName: keyof typeof sounds) => {
	const sound = sounds[soundName];
	sound.currentTime = 0;
	sound.play().catch(console.error);
};
