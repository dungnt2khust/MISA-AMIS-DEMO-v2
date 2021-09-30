<template lang="">
	<div class="formsmall__overlay">
		<div
			class="formsmall"
			:style="{ width: width, 'max-width': width }"
		>
			<div class="formsmall__header">
				<h2 class="formsmall__header-title">{{ title }}</h2>
				<div class="formsmall__header-close">
					<base-help @click.native="help()" />
					<base-cancel @click.native="hideForm()" />
				</div>
			</div>
			<div class="formsmall__body">
				<slot name="body"></slot>
			</div>
			<div class="formsmall__footer fx-space-between">
				<base-button :method="hideForm" label="Huỷ" />
				<div class="fx">
					<base-button label="Cất" class="mr-10" />
					<base-button type="green" label="Cất và Thêm" />
				</div>
			</div>
		</div>
	</div>
</template>
<script>
	// LIBRARY
	import globalComponents from "../../../mixins/globalComponents/globalComponents.js"
	// COMPONENTS
	import BaseButton from "../Button/BaseButton.vue";
	import BaseHelp from "./components/BaseHelp.vue";
	import BaseCancel from "./components/BaseCancel.vue";

	export default {
		name: "BaseFormSmall",
		mixins: [globalComponents],
		components: {
			BaseButton,
			BaseHelp,
			BaseCancel,
		},
		props: {
			title: {
				type: String,
				default: "",
			},
            width: {
                type: String,
                default: ''
            }
		},
		methods: {
			/**
			 * Trợ giúp
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			help() {
				this.callDialog('warn', this.$resourcesVN.NOTIFY.FeatureNotAvaiable);
			},
			/**
			 * Ẩn form
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			hideForm() {
				this.$emit('input', false);
			}
		}
	};
</script>
<style>
	@import url("../../../css/base/form/formsmall.css");
</style>
