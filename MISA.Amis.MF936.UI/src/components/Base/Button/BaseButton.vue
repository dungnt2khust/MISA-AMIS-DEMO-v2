<template lang="">
	<div tabindex="20" v-on="tooltipListeners(tooltip)" @click="method()" class="button" :class="'button--' + type">
		{{ label }}
	</div>
</template>
<script>	
	// LIBRARY
	import globalComponents from '../../../mixins/globalComponents/globalComponents.js'
	export default {
		name: "BaseButton",
		mixins: [globalComponents],
		props: {
			label: {
				type: String,
				default: "",
			},
			method: {
				type: Function,
				default: () => {},
			},
			type: {
				type: String,
				default: "",
			},
			tooltip: {
				type: String,
				default: ""
			}
		},
	};
</script>
<style>
	@import url("../../../css/base/button/button.css");
</style>
