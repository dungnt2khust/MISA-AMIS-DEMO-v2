<template lang="">
	<div class="attach">
		<div class="attach__header">
			<div class="attach__icon"></div>
			<div class="attach__title">Đính kèm</div>
			<div class="attach__desc">Dung lượng tối da 5MB</div>
		</div>
        <div class="attach__input-wrapper">
            <input
                type="file" 
                multiple="multiple"
                class="attach__input"
                @click.prevent="addFile()" 
            />
            <div class="attach__input-placeholder">
                Kéo/thả tệp vào đây hoặc bấm vào đây
            </div>
        </div>	
	</div>
</template>
<script>
    // LIBRARY
    import globalComponents from "../../../../mixins/globalComponents/globalComponents.js"

	export default {
		name: "attach",
        mixins: [globalComponents],
        props: {
            enable: {
                type: Boolean,
                default: true
            }
        },
        methods: {
            /**
             * Thêm file 
             * CreatedBy: NTDUNG (01/10/2021)
             */
            addFile() {
                if (this.enable)
                    this.callDialog("warn", this.$resourcesVN.NOTIFY.FeatureNotAvaiable);
            }
        }
	};
</script>
<style>
    @import url('../../../../css/base/form/components/attach.css');
</style>
