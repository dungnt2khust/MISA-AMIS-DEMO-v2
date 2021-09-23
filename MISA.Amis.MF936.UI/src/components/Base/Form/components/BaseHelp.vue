<template lang="">
	<div v-on="tooltipListeners($resourcesVN.FORM.Help)" class="form__help fx-center"></div>
</template>
<script>
	// LIBRARY
	import globalComponents from "../../../../mixins/globalComponents/globalComponents.js";

	export default {
		name: "BaseHelp",
		mixins: [globalComponents],
	};
</script>
<style>
	@import url("../../../../css/base/form/components.css");
</style>
