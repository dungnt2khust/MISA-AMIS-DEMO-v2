<template lang="">
    <div v-on="tooltipListeners($resourcesVN.FORM.Cancel)" class="form__cancel fx-center">
        
    </div>
</template>
<script>
// LIBRARY 
import globalComponents from "../../../../mixins/globalComponents/globalComponents.js"

export default {
    name: 'BaseCancel',
    mixins: [globalComponents]
}
</script>
<style lang="">
    
</style>