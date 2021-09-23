<template lang="">
    <div v-show="formLargeState" class="formlarge">
        <div class="formlarge__header fx-space-between">
            <div class="fx-center">
                <base-history class="mr-16"/>
                <div class="formlarge__title">Phiếu nhập kho BK026</div>
            </div>
            <div class="form__function fx-center-ver">
                <base-tutorial class="mr-16"/>
                <base-setting class="mr-10"/>
                <base-help/>
                <base-cancel/>
            </div>
        </div>
        <div class="formlarge__body">
            <div class="formlarge__body-part1">
                <div class="formlarge__body-main-info w-75">
                    <div class="w-80 fx-wrap pr-10 border-right">
                        <div class="fx-40">
                            <base-combobox-advance label="Khách hàng"/>
                        </div>
                        <div class="fx-60">
                            <base-input label="Địa chỉ"/>
                        </div>
                        <div class="fx-40">
                            <base-input label="Người giao hàng"/>
                        </div>
                        <div class="fx-60">
                            <base-input label="Diễn giải"/>
                        </div>
                    </div>
                    <div class="w-20"></div>
                </div>
                <div class="formlarge__body-cash w-25"></div>
            </div>
            <div class="formlarge__body-part2">
                <div class="formlarge__table-header fx-space-between fx-center-ver">
                    <span class="formlarge__table-title">Hàng tiền</span>
                    <div class="fx-center">
                        <span class="formlarge__table-filter-title"> Đơn giá nhập kho</span>
                    </div>
                </div>
            </div>
        </div>  
        <div class="formlarge__footer fx-space-between">
            <base-button 
                :label="$resourcesVN.FORM.Cancel"
                :method="hideForm"
                type="dark"/>
            <div class="formlarge__store fx">
                <base-button 
                    :label="$resourcesVN.FORM.Store"
                    type="dark"
                    class="mr-8"
                    />
                <base-button
                    :label="$resourcesVN.FORM.StoreAndPrint"
                    type="green"
                    />
            </div>
        </div>
    </div>
</template>
<script>
// COMPONENTS
import BaseButton from '../Button/BaseButton.vue'
import BaseTutorial from '../BaseTutorial.vue'
import BaseHelp from './components/BaseHelp.vue'
import BaseCancel from './components/BaseCancel.vue'
import BaseSetting from './components/BaseSetting.vue'
import BaseHistory from './components/BaseHistory.vue'
import BaseInput from '../BaseInput.vue'
import BaseComboboxAdvance from '../Select/BaseComboboxAdvance.vue'

export default {
    name: 'BaseFormLarge',
    components: {
        BaseButton,
        BaseTutorial,
        BaseHelp,
        BaseCancel,
        BaseSetting,
        BaseHistory,
        BaseInput,
        BaseComboboxAdvance
    },
    props: {
        formLargeState: {
            type: Boolean,
            default: false
        }
    },
    methods: {
        /**
         * Ẩn form
         * CreatedBy: NTDUNG (23/09/2021)
         */
        hideForm() {
            this.$emit('input', false);
        }
    }
}
</script>
<style>
    @import url('../../../css/base/form/formlarge.css'); 
</style>