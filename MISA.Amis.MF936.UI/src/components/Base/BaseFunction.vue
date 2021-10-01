<template lang="">
	<div class="funtion">
		<div class="function__control-query">
			<div class="function__search">
				<input
					ref="inputSearch"
					type="text"
					class="function__search-input"
					:placeholder="$resourcesVN.FUNCTION.SearchByCodeNameDescription"
					v-on="inputListeners"
				/>
			</div>
			<div
				class="function__reload"
				v-on="tooltipListeners($resourcesVN.FUNCTION.ReloadData)"
				@click="$emit('input', { name: 'Reload' })"
			>
				<div class="function__reload-icon"></div>
			</div>
			<div
				class="function__export"
				v-on="tooltipListeners($resourcesVN.FUNCTION.ExportE)"
				@click="$emit('input', { name: 'Export' })"
			>
				<div class="function__export-icon"></div>
			</div>
		</div>
	</div>
</template>
<script>
	// LIBRARY
	import globalComponents from "../../mixins/globalComponents/globalComponents.js";

	export default {
		name: "BaseFunction",
		mixins: [globalComponents],
		data() {
			return {
				inputTimeout: null,
			};
		},
		computed: {
			inputListeners() {
				return Object.assign({}, this.$listener, {
					input: (event) => {
						clearTimeout(this.inputTimeout);

						this.inputTimeout = setTimeout(() => {
							this.$emit("input", { name: "Search", data: event.target.value });
						}, 500);
					},
				});
			},
		},
		methods: {},
	};
</script>
<style>
	@import url("../../css/base/function.css");
</style>
