<template lang="">
	<div class="listgrid">
		<thead class="listgrid__header">
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
		<tbody class="listgrid__body" :style="{'max-height': bodyHeight}">
			<tr
				@click="choose(itemData)"
				v-for="(itemData, indexData) in listGridData"
				:class="{
					'listgrid--selected': foundSelectedItem(itemData),
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
			valueBind: {
				type: String, 
				default: ''
			},
			vmodelField: {
				type: String,
				default: ''
			},
			subfield: {
				type: String,
				default: ''
			}
		},
		data() {
			return {};
		},
		computed: {
			bodyHeight() {
				if (this.type == "small") {
					return "180px";
				} else {
					return "200px";
				}
			},
		},
		methods: {
			/**
			 * Tìm phần tử được select
			 * @param {Object} itemData
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			foundSelectedItem(itemData) {
				if (!this.subfield)
					return itemData[this.vmodelField] == this.valueBind;
				else 
					return itemData[this.subfield] == this.valueBind;
			},
			/**
			 * Khi chọn vào một lựa chọn thì emit ra ngoài để cập nhật
			 * @param {Object} itemData
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			choose(itemData) {
				if (!this.subfield)
					this.$emit('changeOption', itemData[this.vmodelField]);
				else 
					this.$emit('changeOption', itemData[this.subfield]);
			}
		}
	};
</script>
<style scoped>
	@import url("../../../css/base/select/listgrid.css");
</style>
