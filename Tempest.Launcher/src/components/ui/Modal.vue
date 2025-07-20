<template>
  <div v-if="visible" class="modal-overlay" @click="closeModal">
    <div class="modal-content" @click.stop>
      <div class="modal-header">
        <h2>{{ title }}</h2>
        <button class="close-button" @click="closeModal">×</button>
      </div>
      <div class="modal-body">
        <slot></slot>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
interface Props {
  visible: boolean
  title?: string
}

interface Emits {
  (e: 'close'): void
}

withDefaults(defineProps<Props>(), {
  title: 'Modal'
})

const emit = defineEmits<Emits>()

function closeModal() {
  emit('close')
}
</script>

<style scoped>
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.7);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
}

.modal-content {
  background: var(--c-bg-2, #2a2a2a);
  border-radius: 8px;
  max-width: 90%;
  max-height: 90%;
  overflow: auto;
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.5);
  border: 1px solid var(--c-border, #444);
}

.modal-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 1rem 1.5rem;
  border-bottom: 1px solid var(--c-border, #444);
}

.modal-header h2 {
  margin: 0;
  color: var(--c-text-1, white);
  font-size: 1.5rem;
}

.close-button {
  background: none;
  border: none;
  font-size: 2rem;
  color: var(--c-text-2, #ccc);
  cursor: pointer;
  padding: 0;
  width: 30px;
  height: 30px;
  display: flex;
  align-items: center;
  justify-content: center;
  border-radius: 4px;
  transition: box-shadow 0.3s ease-in-out;
}

.close-button:hover {
	background: linear-gradient(#1cc6fbcc, #0194d4cc, #00a2dacc);
	box-shadow: var(--c-brand-1) 0px 0px 1rem 0.4rem;
	border-color: #1cc6fb;
}

.modal-body {
  padding: 1.5rem;
}
</style>