<script setup lang="ts">
import Navigation from "@/components/ui/Navigation";
import Header from "@/components/layout/Header.vue";
import Button from "@/components/ui/Button.vue";
</script>

<template>
	<Navigation.Content padding="standard">
		<Header text="Builds">
			<Button>Add</Button>
			<Button>Edit</Button>
		</Header>
	</Navigation.Content>
</template>
