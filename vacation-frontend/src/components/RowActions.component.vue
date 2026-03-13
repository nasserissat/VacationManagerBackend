<template>
  <div class="row-actions">
    <button
      class="btn btn-gray btn-sm"
      @click="$emit('edit')"
      :disabled="disabled"
    >
      <fa-icon icon="pen-to-square" />
    </button>

    <button
      class="btn btn-danger btn-sm"
      @click="$emit('cancel')"
      :disabled="disabled"
    >
      <fa-icon icon="ban" />
    </button>
  </div>
</template>

<script setup>
defineProps({
  disabled: {
    type: Boolean,
    default: false
  }
})

defineEmits(['edit', 'cancel'])
</script>