<script lang="ts">
	import type { EventHandler } from "svelte/elements";

	const contextMenu: EventHandler<Event> = (event) => {
		event.preventDefault();
		return false;
	};
	// const ended: EventHandler<Event, HTMLVideoElement> = (event) => {
	//     const video = event.target as HTMLVideoElement | null;
	//     if (!video) return;

	//     video.currentTime = 0;
	//     video.play();
	// };
</script>

<div class="background-blur">
	<img src="/img/logo.png" alt="Tempest Logo" on:contextmenu={contextMenu} />
	<!-- <video on:contextmenu={contextMenu} loop autoplay muted playsinline controls on:ended={ended}>
        <source src="/video/logo-animated.mp4" type="video/mp4" />
    </video> -->
</div>

<style>
	.background-blur {
		border-radius: 100%;
		overflow: hidden;
		filter: blur(10vmax);
		position: fixed;
		left: 150px;
		top: 36px;
		width: calc(110vw - 100px);
		height: calc(110vh - 25px);
		z-index: -10;
	}

	.background-blur * {
		width: 100%;
		height: 100%;
		object-fit: fill;
		-webkit-user-drag: none;
		user-select: none;
		-moz-user-select: none;
		-webkit-user-select: none;
		-ms-user-select: none;
	}
</style>
