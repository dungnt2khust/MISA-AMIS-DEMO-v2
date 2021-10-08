<template lang="">
	<div class="tableinput-wrapper" @scroll="scrollTable()">
		<div class="tableinput__title">Hàng tiền</div>
		<table class="tableinput" :class="{'tableinput--disable': !enable}">
			<thead>
				<tr>
					<th class="fx-center">#</th>
					<th
						v-for="(item, index) in tableStyle"
						:style="{ 'min-width': item['width'] }"
						:class="positionOfRecord(item)"
						:key="index"
					>
						{{ item["name"] }}
					</th>
					<th></th>
				</tr>
			</thead>
			<tbody>
				<tr
					v-for="(itemData, indexData) in tableData"
					v-show="itemData['state'] != 2"	
					:key="indexData"
				>
					<td class="fx-center" >{{tableDisplay.indexOf(itemData) + 1}}</td>
					<td
						v-for="(itemStyle, indexStyle) in tableStyle"
						:class="positionOfRecord(itemStyle)"
						:key="indexStyle"
					>
						<base-combobox-advance
							v-if="
								(itemStyle.type == TableDataStyle.TYPE.Combobox ||
									itemStyle.type == TableDataStyle.TYPE.ComboboxNotAdd)
							"
							:controller="itemStyle['controller']"
							v-model="itemData[itemStyle['vmodel']]"
							:value="itemData[itemStyle['vmodel']]"
							:vmodelField="itemStyle['vmodel']"
							:field="itemStyle['field']"
							:default="
								itemStyle['default']
									? itemData[itemStyle['default']]
									: itemData[itemStyle['field']]
							"
							:display="itemStyle['display']"
							:listGridStyle="itemStyle['style']"
							:data="itemData[itemStyle['data']]"
							:subfield="itemStyle['subfield']"
							:form="itemStyle['form']"
							:index="indexData"
							type="small"
							:syncfield="itemStyle['syncfield']"
							:hasFooter="itemStyle['hasFooter']"
							:disable="!enable"
							:formName="formName"
							:required="itemStyle['required']"
							:name="itemStyle['name']"
							:escapeValue="escapeValue"
						/>
						<base-input
							v-if="(itemStyle.type == TableDataStyle.TYPE.Input)"
							:value="itemData[itemStyle['field']]"
							v-model="itemData[itemStyle['field']]"
							:pos="itemStyle.pos"
							:disable="!enable || !itemStyle['enable']"
							:syncfield="itemStyle['syncfield']"
							:index="indexData"
							:formName="formName"
							:required="itemStyle['required']"
							:name="itemStyle['name']"
							:format="itemStyle['format']"
							:default="itemStyle['default']"
						/>
						<base-input-date
							v-if="(itemStyle.type == TableDataStyle.TYPE.InputDate)"
							:value="itemData[itemStyle['field']]"
							v-model="itemData[itemStyle['field']]"
							:overtoday="itemStyle['overtoday']"
							:disable="!enable"
							:required="itemStyle['required']"
							:name="itemStyle['name']"
							:formName="formName"
						/>
					</td>
					<td>
						<div @click="deleteOneRecord(indexData)" class="tableinput__delete"></div>
					</td>
				</tr>
			</tbody>
			<tfoot>
				<tr>
					<th :key="100"></th>
					<th v-for="(item, index) in tableStyle" :key="index">
						{{ footerValue(item) }}
					</th>
					<th :key="101"></th>
				</tr>	
			</tfoot>
		</table>
		<div class="tableinput__function mt-20 mb-20">
			<base-button :method="addRecord" :enable="enable" label="Thêm dòng" type="small" />
			<base-button :method="addNote" :enable="enable" class="ml-10" label="Thêm ghi chú" type="small" />
			<base-button
				:method="deleteAllRecords"
				class="ml-10"
				label="Xoá hết dòng"
				:enable="enable"
				type="small"
			/>
		</div>
		<base-attach :enable="enable"/>
	</div>
</template>
<script>
	// LIBRARY
	import TableDataStyle from "../../../js/enum/tableDataStyle";
	import VoucherDetailState from "../../../js/enum/voucherDetailState";
	import globalComponents from "../../../mixins/globalComponents/globalComponents.js"
	import methods from "../../../mixins/methods"
	// COMPONENTS
	import BaseInput from "../BaseInput.vue";
	import BaseInputDate from "../BaseInputDate.vue";
	import BaseComboboxAdvance from "../Select/BaseComboboxAdvance.vue";
	import BaseButton from "../Button/BaseButton.vue";
	import BaseAttach from "../Form/components/BaseAttach.vue"

	export default {
		name: "BaseTableInput",
		mixins: [globalComponents, methods],
		components: {
			BaseInput,
			BaseComboboxAdvance,
			BaseButton,
			BaseInputDate,
			BaseAttach
		},
		props: {
			tableStyle: {
				type: Array,
				default: function() {
					return [];
				},
			},
			tableData: {
				type: Array,
				default: function() {
					return [];
				},
			},
			defaultBind: {
				type: Object,
				default: function() {
					return {}
				}
			},
			enable: {
				type: Boolean,
				default: true
			},
			formName: {
				type: String, 
				defautl: ''
			},
			escapeValue: {
				type: [String, Array],
				default: null
			}
		},
		data() {
			return {
				TableDataStyle: TableDataStyle,
			};
		},
		computed: {
			/**
			 * Table đang hiển thị
			 * CreatedBy: NTDUNG (08/10/2021)
			 */
			tableDisplay() {
				var tableData = this.tableData.filter(item => {
					return item['state'] != VoucherDetailState.DELETE;
				});
				return tableData;
			}
		},
		methods: {
			/**
			 * Đặt vị trí cho bản ghi
			 * @param {Object} itemStyle
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
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
				}
				return styleClass;
			},
			/**
			 * Thêm một bản ghi rỗng vào trong bảng
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			addRecord() {
				var rowEmpty = {};
				this.tableStyle.forEach((item) => {
					rowEmpty[item["field"]] = null;
				});
				rowEmpty["state"] = VoucherDetailState.ADD;
				rowEmpty[this.defaultBind['name']] = this.defaultBind['value'];
				var tableData = this.tableData;
				tableData.push(rowEmpty);
				this.changeTableData(tableData);	
			},
			/**
			 * Xoá tất cả các bản ghi
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			deleteAllRecords() {
				var tableData = this.tableData;
				tableData.forEach((item, index) => {	
					if (item['state'] == VoucherDetailState.ADD)  
						tableData.splice(index, 1);
					else 
						item["state"] = VoucherDetailState.DELETE;
				});

				this.changeTableData(tableData);
			},
			/**
			 * Xoá một bản ghi
			 * @param {Number} indexData
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			deleteOneRecord(indexData) {
				if (this.enable) {
					var tableData = this.tableData;
					if (tableData[indexData]['state'] == VoucherDetailState.ADD)
						tableData.splice(indexData, 1);
					else
						tableData[indexData]['state'] = VoucherDetailState.DELETE;
					this.changeTableData(tableData);
				}	
			},
			/**
			 * Thay đổi dữ liệu trong bảng
			 * @param {Array} newValue
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			changeTableData(newValue) {
				this.$emit("input", newValue);
			},
			/**
			 * Thêm ghi chú
			 * CreatedBy: NTDUNG (30/09/2021)
			 */
			addNote() {
				this.callDialog("warn", this.$resourcesVN.NOTIFY.FeatureNotAvaiable);
			},
			/**
			 * Giá trị bind vào footer
			 * @param {Object} item
			 * CreatedBy: NTDUNG (01/10/2021) 
			 */
			footerValue(item) {
				if (item["total"]) {
					var total = 0;
					this.tableData.forEach(data=> {
						if (data['state'] != VoucherDetailState.DELETE)
							total += Number(data[item['field']] ? data[item['field']] : 0);
					});
					return this.formatMoney(total);
				}
			},
			scrollTable() {
				this.$bus.$emit('hideListGrid');
			},
		},
	};
</script>
<style scoped>
	@import url("../../../css/base/table/tableinput.css");
</style>
