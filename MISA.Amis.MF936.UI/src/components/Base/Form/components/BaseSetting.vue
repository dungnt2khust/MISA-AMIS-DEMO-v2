<template lang="">
	<div
		v-on="tooltipListeners($resourcesVN.FORM.Setting)"
		@click="setting()"
		class="form__setting fx-center"
	></div>
</template>
<script>
	// LIBRARY
	import globalComponents from "../../../../mixins/globalComponents/globalComponents.js";

	export default {
		name: "BaseSetting",
		mixins: [globalComponents],
		methods: {
			/**
			 * Cài đặt 
			 * CreatedBy: NTDUNG (30/09/2021)
			 */
			setting() {
				this.callDialog("warn", this.$resourcesVN.NOTIFY.FeatureNotAvaiable);
			}
		}
	};
</script>
<style lang=""></style>
