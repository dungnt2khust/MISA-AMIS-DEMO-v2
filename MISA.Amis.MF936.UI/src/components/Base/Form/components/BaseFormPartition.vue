<template lang="">
	<div
		class="formpartition mt-20"
		:class="{ 'formpartition--show': formPartitionState }"
	>
		<div
			@click="formPartitionState = !formPartitionState"
			class="formpartition__header"
		>
			<div class="formpartition__icon"></div>
			<div class="formpartition__title">{{ label }}</div>
		</div>
		<div v-show="formPartitionState" class="formpartition__body">
			<slot name="body"></slot>
		</div>
	</div>
</template>
<script>
	export default {
		name: "BaseFormPartition",
		props: {
			label: {
				type: String,
				default: "",
			},
		},
		data() {
			return {
				formPartitionState: false,
			};
		},
	};
</script>
<style>
	@import url("../../../../css/base/form/components/formpartition.css");
</style>
