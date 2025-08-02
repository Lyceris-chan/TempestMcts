<script lang="ts">
    import Button from "$lib/components/ui/Button.svelte";
</script>

<div class="header">
    <Button
    kind="icon"
    title="Go back"
    href="/servers"
    >
    {"<"}
    </Button>
    <h1>Create Server</h1>
</div>
<div class="container">
    <div class="elements">
        <div class="map-and-gamemode">
            <div class="map">
                <img src="https://raw.githubusercontent.com/PaladinsDev/Assets/refs/heads/master/loading-screens/Loading_Quarry.png" alt="tt">
                Splitstone Quarry
            </div>
            <div class="gamemode">
                <h1>Rules</h1>
                Two teams face off against each other. Control the capture point and then push the payload.
            </div>
        </div>
        <div class="gamemode-settings">
            <label for="gamemode-select">Select Gamemode</label>
            <select name="gamemode-select" id="gamemode-select">
                <option value="siege">Siege</option>
                <option value="onslaught">Onslaught</option>
                <option value="tdm">Team Deathmatch</option>
                <option value="payload">Payload</option>
                <option value="totalmayhem">Total Mayhem</option>
            </select>
            <label for="map-select">Select Map</label>
            <select name="map-select" id="map-select">
                <option value="siege">Splitstone Quarry</option>
                <option value="onslaught">Onslaught</option>
                <option value="tdm">Team Deathmatch</option>
                <option value="payload">Payload</option>
                <option value="totalmayhem">Total Mayhem</option>
            </select>
        </div>
    </div>
</div>

<style>
    .header {
        display: flex;
        justify-content: start;
        align-items: center;
        gap: 1rem;
    }
    .container {
        display: flex;
        justify-content: center;
        align-items: center;
    }
    .elements {
        display: flex;
        justify-content: space-evenly;
        align-items: center;
        gap: 25px;
    }
    .map-and-gamemode {
        width: 40%;
        display: flex;
        flex-direction: column;
    }
    .gamemode-settings {
        display: flex;
        flex-direction: column;
    }
</style>