<template lang="">
    <div v-if="loadingState" class="loading">
        <div class="loading__icon"></div> 
    </div>
</template>
<script>
export default {
    name: 'TheLoading',
    data()  {
        return {
            loadingState: false
        }
    },
    created() {
        /**
         * Show loading 
         * CreatedBy: NTDUNG (03/09/2021)
         */
        this.$bus.$on('showLoading', () => {
            this.loadingState = true;
        });
        /**
         * Hide loading
         * CreatedBy: NTDUNG (03/09/2021)
         */
        this.$bus.$on('hideLoading', () => {
            this.loadingState = false;
        });
    }
}
</script>
<style lang="">
    
</style>