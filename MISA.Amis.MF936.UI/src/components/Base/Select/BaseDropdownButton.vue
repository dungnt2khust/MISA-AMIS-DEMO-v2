<template lang="">
	<div
		@click="dropdownButtonOnClick()"
		class="dropdownbutton"
		:class="{ 'dropdownbutton--selected': showList }"
	>
		<div class="dropdownbutton__icon"></div>
	</div>
</template>
<script>
	export default {
		name: "BaseDropdownButton",
		props: {
			showList: {
				type: Boolean,
				default: false
			},
			focusState: {
				type: Boolean,
				default: false
			},
			method: {
				type: Function,
				default: () => {}
			}
		},
		methods: {
			dropdownButtonOnClick() {
				this.method();
            
				this.$emit('input', !this.showList);
				
			},
		},
	};
</script>
<style>
	@import url("../../../css/base/select/dropdownbutton.css");
</style>
