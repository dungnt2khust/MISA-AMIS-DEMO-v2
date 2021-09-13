<template lang="">
    <div id="header">
        <div class="header__left">
            <div v-if="menuState" @click="$emit('toggleMenu')" class="header__toggle">
                <div class="header__toggle-icon"></div>
            </div>
            <div class="header__dropdown">
                <work-modes/>
            </div>
        </div>
        <div class="header__right">
            <div class="header__notify">
                <div class="header__notify-icon"></div>
            </div>
            <div class="header__account">
                <div class="header__avatar"></div>
                <div class="header__name">Nguyễn Tiến Dũng</div>
                <div class="header__account-dropdown"></div>
            </div>
        </div>
    </div>
</template>
<script>
    // COMPONENTS
    import BaseSelectBox from '../Base/BaseSelectBox.vue'

    export default {
        name: 'TheHeader',
        components: {
            WorkModes: BaseSelectBox
        },
        props: {
            menuState: {
                type: Boolean,
                default: false
            }
        }
    }
</script>
<style lang="">
    
</style>