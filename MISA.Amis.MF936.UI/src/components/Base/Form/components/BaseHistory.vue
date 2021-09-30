<template lang="">
    <div v-on="tooltipListeners('Lịch sử')" @click="history()" class="form__history fx-center">
        
    </div>
</template>
<script>
// LIBRARY
import globalComponents from '../../../../mixins/globalComponents/globalComponents.js'
export default {
    name: 'BaseHistory',
    mixins: [globalComponents],
    methods: {
        /**
         * Lịch sử
         * CreatedBy: NTDUNG (30/09/2021)
         */
        history() {
            this.callDialog("warn", this.$resourcesVN.NOTIFY.FeatureNotAvaiable);
        }
    }
}
</script>
<style>
    
</style>