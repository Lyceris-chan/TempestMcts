<script setup lang="ts">
import { RouterView } from "vue-router";
import Navigation from "./components/ui/Navigation";
</script>

<template>
	<main>
		<Navigation.Root>
			<Navigation.Section>
				<!-- <Navigation.Header>Tempest</Navigation.Header> -->
				<Navigation.Link to="/" title="Home" />
				<Navigation.Link to="/builds" title="Builds" />
				<Navigation.Link to="/multiplayer" title="Multiplayer" />
				<Navigation.Link to="/servers" title="Servers" />
				<Navigation.Link to="/settings" title="Settings" />
			</Navigation.Section>
			<Navigation.Section> </Navigation.Section>
		</Navigation.Root>
		<RouterView />
	</main>
</template>

<style>
:root {
	overflow: hidden;
}
</style>
