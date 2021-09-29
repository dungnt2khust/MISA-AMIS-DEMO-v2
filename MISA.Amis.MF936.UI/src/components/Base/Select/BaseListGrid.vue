<template lang="">
	<div class="listgrid">
		<div class="listgrid__header">
			<thead>
				<tr>
					<th
						v-for="(item, index) in listGridStyle"
						:style="{
							'min-width': item['width'],
						}"
						:key="index"
					>
						{{ item["name"] }}
					</th>
				</tr>
			</thead>
		</div>
		<div class="listgrid__body" :style="{ 'max-height': bodyHeight }">
			<tbody>
				<tr
					@click="chooseOption(indexData)"
					v-for="(itemData, indexData) in listGridData"
					:class="{
						'listgrid--selected': indexData == 1,
					}"
					:key="indexData"
				>
					<td
						v-for="(itemStyle, indexStyle) in listGridStyle"
						:style="{ 'min-width': itemStyle['width'] }"
						:key="indexStyle"
					>
						{{ itemData[itemStyle["field"]] }}
					</td>
				</tr>
			</tbody>
		</div>
		<div v-if="type == 'small'" class="listgrid__footer">
			<slot name="footer"></slot>
		</div>
	</div>
</template>
<script>
	export default {
		name: "BaseListGrid",
		props: {
			type: {
				type: String,
				default: "",
			},
			listSelected: {
				type: Array,
				default: null,
			},
			listGridStyle: {
				type: Array,
				default: function() {
					return [];
				},
			},
			listGridData: {
				type: Array,
				default: function() {},
			},
		},
		data() {
			return {};
		},
		computed: {
			bodyHeight() {
				if (this.type == "small") {
					return "180px";
				} else {
					return "";
				}
			},
		}	
	};
</script>
<style scoped>
	@import url("../../../css/base/select/listgrid.css");
</style>
