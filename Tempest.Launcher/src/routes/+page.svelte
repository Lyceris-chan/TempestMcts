<script lang="ts">
    import gamemodesData from '$lib/assets/gamemodes/gamemodes.json';

</script>

<div class="header">
    <h1>Welcome to Tempest</h1>
    <h3>00:03 (Siege)</h3>
</div>
<h2>Enter queue</h2>
<div class="gamemode-queue">
    {#each gamemodesData as gamemode}
        <button class="gamemode-card">
            <img src={gamemode.icon} alt="">
            <p>{gamemode.name}</p>
        </button>
    {/each}
</div>
<style>
    .header {
        text-align: center;
    }
    .header > h1 {
        text-shadow: 1px 1px 2px var(--color-primary), 0 0 25px darkblue, 0 0 5px #1cc6fb;
    }
    .gamemode-queue {
        margin: 45px auto;
        padding: 25px;
        display: flex;
        justify-content: space-evenly;
        align-items: center;
        text-align: center;
        gap: 45px;
    }
    .gamemode-card {
        border: 2px solid #00a2dacc;
        transition: rotate 0.2s ease-in-out, background 0.2s ease-in-out, box-shadow 0.2s ease-in-out;
        font-weight: 500;
    }
    .gamemode-card:hover {
        background: linear-gradient(#1cc6fbcc, #0194d4cc, #00a2dacc);
		box-shadow: #0f92ce 0px 0px 1rem 0.1rem;
		border-color: #1cc6fb;
        rotate: 1deg;
        transition: rotate 0.2s ease-in-out, background 0.2s ease-in-out, box-shadow 0.2s ease-in-out;
    }
    button {
		all: unset;
		cursor: pointer;
	}
</style>