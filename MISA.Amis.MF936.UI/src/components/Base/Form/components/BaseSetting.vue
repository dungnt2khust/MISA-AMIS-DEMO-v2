<template lang="">
	<div
		v-on="tooltipListeners($resourcesVN.FORM.Setting)"
		class="form__setting fx-center"
	></div>
</template>
<script>
	// LIBRARY
	import globalComponents from "../../../../mixins/globalComponents/globalComponents.js";

	export default {
		name: "BaseSetting",
		mixins: [globalComponents],
	};
</script>
<style lang=""></style>
