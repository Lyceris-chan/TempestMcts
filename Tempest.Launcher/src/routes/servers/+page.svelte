<script lang="ts">
	import Button from "$lib/components/ui/Button.svelte";
	import { Server, serverList } from "$lib/rpc";
	import { onMount } from "svelte";

	const list = $state<Server[]>([]);

	const refresh = async () => {
		try {
			const serverStream = serverList.getServers({});

			list.length = 0;

			for await (const server of serverStream.responses) {
				list.push(server);
			}
		} catch (error) {
			console.error("Failed to fetch server list:", error);
		}
	};

	onMount(refresh);
</script>

<h1>Servers</h1>
<Button onclick={refresh}>Refresh</Button>

<table class="server-list">
	<tbody>
		{#each list as server}
			<tr class="server-entry">
				<th>{server.name}</th>
				<td>{server.version}</td>
				<td>{server.map.length == 0 ? "Unknown" : server.map}</td>
				<td>{server.players}/{server.maxPlayers}</td>
				<td class="button-row">
					<Button>Join</Button>
					<Button>Favorite</Button>
				</td>
			</tr>
		{/each}
	</tbody>
</table>

<style>
	table, tbody {
		border-collapse: separate;
		border-spacing: 0 var(--spacing-md);
		width: 100%;
	}

	.server-entry {
		background: linear-gradient(to top, var(--border-primary) 0%, transparent 75%);
		outline: 1px solid var(--border-primary);
		box-shadow:
			rgba(0, 0, 0, 0.3) 0px 3px 6px 0px,
			rgba(0, 0, 0, 0.1) 0px 3px 6px 0px;
		width: 100%;
		text-align: left;
	}

	.server-entry td, .server-entry th {
		padding: var(--spacing-sm) var(--spacing-md);
	}

	.button-row {
		display: flex;
		gap: var(--spacing-sm);
	}

	.button-row :global(*) {
		width: 100%;
		max-width: 8rem;
	}
</style>
