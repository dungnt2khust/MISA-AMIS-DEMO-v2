<template lang="">
	<div
		tabindex="20"
		v-on="tooltipListeners(tooltip)"
		@click="buttonOnClick()"
		class="button"
		:class="[{'button--disable': !enable}, 'button--' + type]"
	>
		{{ label }}
	</div>
</template>
<script>
	// LIBRARY
	import globalComponents from "../../../mixins/globalComponents/globalComponents.js";
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
				default: "",
			},
			enable: {
				type: Boolean,
				default: true,
			},
		},
		methods: {
			/**
			 * Nhấn nút
			 * CreatedBy: NTDUNG (30/09/2021)
			 */
			buttonOnClick() {
				if (this.enable) 
					this.method();
			}
		}
	};
</script>
<style>
	@import url("../../../css/base/button/button.css");
</style>
