<template>
    <h1 class="page-title">{{ title }}</h1>
    <slot />
</template>

<script setup>
defineProps({
  title: {
    type: String,
    required: true
  }
})
</script>