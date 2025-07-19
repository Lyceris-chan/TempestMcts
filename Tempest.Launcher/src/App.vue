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
				<Navigation.Link to="/servers" title="Servers" />
			</Navigation.Section>
			<Navigation.Section> </Navigation.Section>
		</Navigation.Root>
		<RouterView />
	</main>
</template>
