<template lang="">
	<div class="listgrid">
		<div class="listgrid__header">
			<thead>
				<tr>
					<th
						v-for="(item, index) in listGridStyle"
						:style="{
							'max-width': item['width'],
							'text-transform': 'none',
						}"
						:key="index"
					>
						{{ item["name"] }}
					</th>
				</tr>
			</thead>
		</div>
		<div class="listgrid__body" :style="{ 'max-height': '160px' }">
			<tbody>
				<tr
					@click="chooseOption(indexData)"
					v-for="(itemData, indexData) in listGridData"
					:class="{
						'listgrid--multi-selected': selectedOption(indexData),
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
					<td>
						<div
							v-if="
								listSelected.findIndex((item) => {
									return item == indexData;
								}) != -1
							"
							class="listgrid__checked"
						></div>
					</td>
				</tr>
			</tbody>
		</div>
	</div>
</template>
<script>
	export default {
		name: "BaseListGrid",
		props: {
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
		methods: {
			/**
			 * Chọn một item trong list
			 * @param {Number} index
			 * CreatedBy: NTDUNG (26/09/2021)
			 */
			chooseOption(index) {
				var listSelected = this.listSelected;
				var foundIdx = listSelected.findIndex((item) => {
					return item == index;
				});
				if (foundIdx == -1) {
					listSelected.push(index);
				} else {
					listSelected.splice(foundIdx, 1);
				}
				this.$emit("input", listSelected);
			},
			/**
			 * Kiểm tra đâu là lựa chọn đang được chọn
			 * @param {Number} index
			 * CreatedBy: NTDUNG (26/09/2021)
			 */
			selectedOption(index) {
				if (this.type == "multichoose") {
					var foundIdx = this.listSelected.findIndex((item) => {
						return item == index;
					});
					if (foundIdx != -1) return true;
					else return false;
				}
			},
		},
	};
</script>
<style>
	@import url("../../../css/base/select/listgrid.css");
</style>
