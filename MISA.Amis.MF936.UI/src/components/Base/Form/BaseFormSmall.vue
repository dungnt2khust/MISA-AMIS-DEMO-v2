<template lang="">
	<div class="formsmall__overlay">
		<div
			class="formsmall"
			:style="{ width: width, 'max-width': width }"
		>
			<div class="formsmall__header">
				<h2 class="formsmall__header-title">{{ title }}</h2>
				<div class="formsmall__header-close">
					<base-help />
					<base-cancel />
				</div>
			</div>
			<div class="formsmall__body">
				<slot name="body"></slot>
			</div>
			<div class="formsmall__footer fx-space-between">
				<base-button label="Huỷ" />
				<div class="fx">
					<base-button label="Cất" class="mr-10" />
					<base-button type="green" label="Cất và Thêm" />
				</div>
			</div>
		</div>
	</div>
</template>
<script>
	// COMPONENTS
	import BaseButton from "../Button/BaseButton.vue";
	import BaseHelp from "./components/BaseHelp.vue";
	import BaseCancel from "./components/BaseCancel.vue";

	export default {
		name: "BaseFormSmall",
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
	};
</script>
<style>
	@import url("../../../css/base/form/formsmall.css");
</style>
