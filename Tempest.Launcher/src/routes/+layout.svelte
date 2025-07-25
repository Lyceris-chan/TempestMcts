<script lang="ts">
	import "@fontsource-variable/montserrat";
	import "$lib/styles/global.css";
	import "$lib/styles/theme.css";
	import AppShell from "$lib/components/layout/AppShell.svelte";
</script>

<AppShell>
	<slot />
</AppShell>
