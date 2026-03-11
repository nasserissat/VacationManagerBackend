<script setup lang="ts">
import { onBeforeUnmount, onMounted, ref, watch } from 'vue'

const props = defineProps<{
  modelValue: boolean
}>()

const emit = defineEmits<{
  (e: 'update:modelValue', value: boolean): void
  (e: 'dismiss'): void
  (e: 'close'): void
}>()

const content = ref<HTMLDivElement | null>(null)
const dismissing = ref(false)

function isInside(target: EventTarget | null) {
  const element = content.value
  if (!element || !(target instanceof Node)) return false
  return element === target || element.contains(target)
}

function down(event: MouseEvent) {
  dismissing.value = !isInside(event.target)
}

function up(event: MouseEvent) {
  if (!isInside(event.target) && dismissing.value) {
    emit('dismiss')
    emit('update:modelValue', false)
  }
}

function handleClose() {
  emit('close')
  emit('update:modelValue', false)
}

function lockBodyScroll() {
  document.body.style.overflow = 'hidden'
}

function unlockBodyScroll() {
  document.body.style.overflow = 'auto'
}

watch(
  () => props.modelValue,
  (value) => {
    if (value) lockBodyScroll()
    else unlockBodyScroll()
  },
  { immediate: true }
)

onMounted(() => {
  if (props.modelValue) {
    lockBodyScroll()
  }
})

onBeforeUnmount(() => {
  unlockBodyScroll()
})
</script>

<template>
  <Teleport to="body">
    <div
      v-if="modelValue"
      class="base-modal-overlay"
      @mousedown="down"
      @mouseup="up"
    >
      <div ref="content" class="base-modal-content">
        <button
          class="base-modal-close"
          type="button"
          title="Cerrar"
          @click="handleClose"
        >
          ×
        </button>

        <slot />
      </div>
    </div>
  </Teleport>
</template>

<style scoped>
.base-modal-overlay {
  position: fixed;
  inset: 0;
  z-index: 9999;

  display: grid;
  place-items: center;

  padding: 24px;

  background: rgba(0, 0, 0, 0.10);
  backdrop-filter: blur(6px);
}

.base-modal-content {
  position: relative;
  max-width: 900px;
  max-height: 90vh;
  overflow-y: auto;

  background: var(--white, #ffffff);
  border: 1px solid var(--gray-100, #f1f1f1);
  border-radius: 16px;
  box-shadow: 0 12px 32px rgba(0, 0, 0, 0.14);
  padding: 24px;
}

.base-modal-close {
  position: absolute;
  top: 8px;
  right: 12px;

  border: none;
  background: transparent;

  font-size: 28px;
  line-height: 1;
  cursor: pointer;

  color: var(--primary, #1B4F8C);

  transition: transform 0.18s ease, color 0.18s ease;
}

.base-modal-close:hover {
  transform: scale(1.1);
  color: var(--danger, #ef4444);
}

@media (max-width: 768px) {
  .base-modal-overlay {
    padding: 16px 12px;
  }

  .base-modal-content {
    padding: 24px 20px 20px;
  }
}
</style>
