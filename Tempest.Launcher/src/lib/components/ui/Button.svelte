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
		transition: all 0.3s var(--curve);
	}

	button:not(:disabled):hover {
		background: linear-gradient(#1cc6fbcc, #0194d4cc, #00a2dacc);
		box-shadow: #0f92ce 0px 0px 1rem 0.1rem;
		border-color: #1cc6fb;
		transform: translateY(-1px);
	}

	button:not(:disabled):active {
		background: linear-gradient(#0f92cecc, #1f72a4cc, #137da9cc);
		box-shadow: #0f92ce 0px 0px 1rem 0.1rem;
		border-color: #0f92ce;
		transform: translateY(-2px);
	}

	button:disabled {
		opacity: 0.5;
		cursor: auto;
		background: linear-gradient(#666666cc, #555555cc, #444444cc);
		border-color: #666666;
		box-shadow: none;
	}

	.icon {
		display: flex;
		align-items: center;
		justify-content: center;
		font-size: 1.6rem;
		width: 2.4rem;
		height: 2.4rem;
	}
</style>
