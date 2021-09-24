<template lang="">
	<div
		v-on="tooltipListeners($resourcesVN.FORM.Cancel)"
		@click="method()"
		class="form__cancel fx-center"
	></div>
</template>
<script>
	// LIBRARY
	import globalComponents from "../../../../mixins/globalComponents/globalComponents.js";

	export default {
		name: "BaseCancel",
		mixins: [globalComponents],
		props: {
			method: {
				type: Function,
				default: () => {},
			},
		},
	};
</script>
<style lang=""></style>
