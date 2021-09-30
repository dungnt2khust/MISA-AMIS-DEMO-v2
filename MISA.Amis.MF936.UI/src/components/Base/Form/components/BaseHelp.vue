<template lang="">
	<div v-on="tooltipListeners($resourcesVN.FORM.Help)" @click="help()" class="form__help fx-center"></div>
</template>
<script>
	// LIBRARY
	import globalComponents from "../../../../mixins/globalComponents/globalComponents.js";

	export default {
		name: "BaseHelp",
		mixins: [globalComponents],
		methods: {
			/**
			 * Trợ giúp
			 * CreatedBy: NTDUNG (30/09/2021)
			 */
			help() {
				this.callDialog("warn", this.$resourcesVN.NOTIFY.FeatureNotAvaiable);
			}
		}
	};
</script>
<style>
	@import url("../../../../css/base/form/components/components.css");
</style>
