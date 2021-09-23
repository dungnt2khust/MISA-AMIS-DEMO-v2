<template lang="">
	<div tabindex="0" @click="method()" class="button" :class="'button--' + type">
		{{ label }}
	</div>
</template>
<script>
	export default {
		name: "BaseButton",
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
		},
	};
</script>
<style>
	@import url("../../../css/base/button/button.css");
</style>
