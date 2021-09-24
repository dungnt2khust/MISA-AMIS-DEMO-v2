<template lang="">
    <div class="commodity">
        COMMODITY
    </div>
</template>
<script>

export default {
    name: 'WarehouseCommodity',
    components: {
    },
    methods: {
        hideForm() {
            this.$emit('input', false);
        }
    }
}
</script>
<style>
    
</style>