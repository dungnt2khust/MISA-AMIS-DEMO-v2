<template lang="">
	<div
		v-show="listGridState"
		ref="listgrid"
		class="listgrid"
		:style="{
			top: top ? top + 'px' : 'unset',
			left: left ? left + 'px' : 'unset',
			bottom: bottom ? bottom + 'px' : 'unset',
		}"
		v-on="listGridListeners"
	>
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
		<tbody class="listgrid__body" :style="{ 'max-height': bodyHeight }">
			<tr
				@click="choose(itemData)"
				v-for="(itemData, indexData) in listGridData"
				:class="[
					{
						'listgrid--selected': indexData == currIdx,
					},
				]"
				:key="indexData"
			>
				<td
					v-for="(itemStyle, indexStyle) in listGridStyle"
					:style="{ 'min-width': itemStyle['width'] }"
					:class="positionOfRecord(itemStyle)"
					:key="indexStyle"
				>
					{{ itemData[itemStyle["field"]] }}
				</td>
			</tr>
		</tbody>
		<div v-if="!listGridData.length" class="listgrid__nodata">
			{{ $resourcesVN.NOTIFY.Nodata }}
		</div>
		<div v-if="hasFooter" class="listgrid__footer">
			<div @click="showForm()" class="comboboxadvance__add">
				<div class="comboboxadvance__icon"></div>
				Thêm mới (F9)
			</div>
		</div>
	</div>
</template>
<script>
	// LIBRARY
	import TableDataStyle from "../../../js/enum/tableDataStyle";
	export default {
		name: "BaseListGrid",
		data() {
			return {
				listGridState: false,
				type: null,
				listSelected: [],
				listGridStyle: [],
				listGridData: [],
				valueBind: null,
				vmodelField: null,
				subfield: null,
				controller: null,
				hasFooter: null,
				form: "",
				size: {},
				name: "",
				currIdx: -1,
				height: null,
				top: 0,
				left: 0,
				bottom: 0,
			};
		},
		created() {
			this.$bus.$on("showListGrid", (data) => {
				this.type = data["type"];
				this.listSelected = data["listSelected"];
				this.listGridStyle = data["listGridStyle"];
				this.listGridData = data["listGridData"];
				this.valueBind = data["valueBind"];
				this.subfield = data["subfield"];
				this.controller = data["controller"];
				this.hasFooter = data["hasFooter"];
				this.vmodelField = data["vmodelField"];
				this.size = data["size"];
				this.form = data["form"];
				this.name = data["name"];
				this.enable = data["enable"];
				this.index = data["index"];

				this.listGridState = true;

				this.currIdx = this.listGridData.findIndex((item) => {
					if (this.subfield) {
						return item[this.subfield] == this.valueBind;
					} else {
						return item[this.vmodelField] == this.valueBind;
					}
				});

				this.left = this.size.left;

				if (this.size.bottom + 200 + 60 > window.innerHeight) {
					this.bottom = window.innerHeight - this.size.top + 4;
					this.top = 0;
				} else {
					this.top = this.size.bottom + 4;
					this.bottom = 0;
				}
			});

			this.$bus.$on("hideListGrid", () => {
				this.listGridState = false;
			});
		},
		computed: {
			bodyHeight() {
				if (this.type == "small") {
					return "180px";
				} else {
					return "200px";
				}
			},
			listGridListeners() {
				return Object.assign({}, this.$listener, {
					keydown: (event) => {
						console.log(event);
					},
				});
			},
		},
		methods: {
			positionOfRecord(itemStyle) {
				var styleClass = {};
				switch (itemStyle.pos) {
					case TableDataStyle.POS.Left:
						styleClass["text-align-left"] = true;
						break;
					case TableDataStyle.POS.Center:
						styleClass["text-align-center"] = true;
						break;
					case TableDataStyle.POS.Right:
						styleClass["text-align-right"] = true;
						break;
					default:
						styleClass["text-align-left"] = true;
						break;
				}
				return styleClass;
			},
			/**
			 * Khi chọn vào một lựa chọn thì emit ra ngoài để cập nhật
			 * @param {Object} itemData
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			choose(itemData) {
				if (!this.subfield)
					this.$bus.$emit(
						"changeOption" + this.name,
						itemData[this.vmodelField],
						this.index
					);
				else
					this.$bus.$emit(
						"changeOption" + this.name,
						itemData[this.subfield],
						this.index
					);
				this.listGridState = false;
			},
			/**
			 * Mở form
			 * CreatedBy: NTDUNG (30/09/2021)
			 */
			showForm() {
				this.listGridState = false;
				this.$bus.$emit(this.form);
			},
		},
	};
</script>
<style scoped>
	@import url("../../../css/base/select/listgrid.css");
</style>
