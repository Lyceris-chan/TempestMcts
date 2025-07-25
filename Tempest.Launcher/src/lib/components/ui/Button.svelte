<script lang="ts">
	import type { HTMLButtonAttributes } from "svelte/elements";

	interface Props extends HTMLButtonAttributes {
		icon?: boolean;
	}

	let { children, icon, ...props }: Props = $props();
</script>

<button class={[{ icon }]} {...props}>
	{@render children?.()}
</button>

<style>
	button {
		background: linear-gradient(#0f92cecc, #1f72a4cc, #137da9cc);
		box-shadow:
			rgba(0, 0, 0, 0.6) 0px 3px 6px 0px,
			rgba(0, 0, 0, 0.3) 0px 3px 6px 0px;
		color: inherit;
		border: 2px solid var(--color-primary);
		border-radius: var(--border-radius);
		padding: 0.5rem 1rem;
		cursor: pointer;
		font-weight: 700;
	}

	button:hover {
		background: linear-gradient(#1cc6fbcc, #0194d4cc, #00a2dacc);
		box-shadow: #0f92ce 0px 0px 1rem 0.1rem;
		border-color: #1cc6fb;
	}

	button:active {
		background: linear-gradient(#0f92cecc, #1f72a4cc, #137da9cc);
		box-shadow: #0f92ce 0px 0px 1rem 0.1rem;
		border-color: #0f92ce;
	}

	.icon {
		display: flex;
		align-items: center;
		justify-content: center;
		width: 2.4rem;
		height: 2.4rem;
	}
</style>
