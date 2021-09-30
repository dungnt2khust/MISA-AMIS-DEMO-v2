<template lang="">
    <div @click="tutorial()" class="tutorial">
        <div class="tutorial__icon"></div>
        <span class="tutorial__text">
            {{ $resourcesVN.COMMON.Tutorial }}</span
        >
    </div>
</template>
<script>
    // LIBRARY
    import globalComponents from '../../mixins/globalComponents/globalComponents.js'
    export default {
        name: 'BaseTutorial',
        mixins: [globalComponents],
        methods: {
            /**
             * Hướng dẫn
             * CreatedBy: NTDUNG (30/09/2021)
             */
            tutorial() {
                this.callDialog("warn", this.$resourcesVN.NOTIFY.FeatureNotAvaiable);
            }
        }
    }
</script>
<style>
    @import url('../../css/base/tutorial.css'); 
</style>