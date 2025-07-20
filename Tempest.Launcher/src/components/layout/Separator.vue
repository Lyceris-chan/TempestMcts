<script setup lang="ts">
interface Props {
    orientation?: 'horizontal' | 'vertical',
}

withDefaults(defineProps<Props>(), {
    orientation: 'vertical',
})
</script>

<template>
  <div class="separator" :class="{ vertical: orientation === 'vertical' }"></div>
</template>

<style scoped>
.separator {
  background-color: rgba(255, 255, 255, 0.1);
  border: none;
}

.separator:not(.vertical) {
  width: 100%;
  height: 2px;
  margin: 1rem 0;
}

.separator.vertical {
  width: 2px;
  height: 100vh;
  margin: 0 1rem;
  align-self: stretch;
}
</style>