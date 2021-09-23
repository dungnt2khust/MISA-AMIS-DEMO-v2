<template lang="">
	<div class="listgrid">
		<div class="listgrid__header">
			<thead>
				<tr>
					<th
						v-for="(item, index) in listGridStyle"
						:style="{ 'min-width': item['width'] }"
						:key="index"
					>
						{{ item["name"] }}
					</th>
				</tr>
			</thead>
		</div>
		<div class="listgrid__body">
			<tbody>
				<tr
					v-for="(itemData, indexData) in listGridData"
					:class="{'listgrid--selected': indexData == 1}"
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
	</div>
</template>
<script>
	export default {
		name: "BaseListGrid",
		data() {
			return {
				listGridStyle: [
					{ name: "Mã đối tượng", field: "object_code", width: "150px" },
					{ name: "Tên đối tượng", field: "object_name", width: "200px" },
					{ name: "Mã số thuế", field: "tax_code", width: "150px" },
					{ name: "Địa chỉ", field: "address", width: "200px" },
					{ name: "Số điện thoại", field: "phone_number", width: "100px" },
				],
				listGridData: [
					{
						object_code: "KH001",
						object_name: "Tên đối tượng",
						tax_code: "Mã số thuế",
						address: "Địa chỉ",
						phone_number: "Số điện thoại",
					},
					{
						object_code: "KH001",
						object_name: "Tên đối tượng",
						tax_code: "Mã số thuế",
						address: "Địa chỉ",
						phone_number: "Số điện thoại",
					},
					{
						object_code: "KH001",
						object_name: "Tên đối tượng",
						tax_code: "Mã số thuế",
						address: "Địa chỉ",
						phone_number: "Số điện thoại",
					},
					{
						object_code: "KH001",
						object_name: "Tên đối tượng",
						tax_code: "Mã số thuế",
						address: "Địa chỉ",
						phone_number: "Số điện thoại",
					},
					{
						object_code: "KH001",
						object_name: "Tên đối tượng",
						tax_code: "Mã số thuế",
						address: "Địa chỉ",
						phone_number: "Số điện thoại",
					},
					{
						object_code: "KH001",
						object_name: "Tên đối tượng",
						tax_code: "Mã số thuế",
						address: "Địa chỉ",
						phone_number: "Số điện thoại",
					},
					{
						object_code: "KH001",
						object_name: "Tên đối tượng",
						tax_code: "Mã số thuế",
						address: "Địa chỉ",
						phone_number: "Số điện thoại",
					},
					{
						object_code: "KH001",
						object_name: "Tên đối tượng",
						tax_code: "Mã số thuế",
						address: "Địa chỉ",
						phone_number: "Số điện thoại",
					},
					{
						object_code: "KH001",
						object_name: "Tên đối tượng",
						tax_code: "Mã số thuế",
						address: "Địa chỉ",
						phone_number: "Số điện thoại",
					},
					{
						object_code: "KH001",
						object_name: "Tên đối tượng",
						tax_code: "Mã số thuế",
						address: "Địa chỉ",
						phone_number: "Số điện thoại",
					},
					{
						object_code: "KH001",
						object_name: "Tên đối tượng",
						tax_code: "Mã số thuế",
						address: "Địa chỉ",
						phone_number: "Số điện thoại",
					},
					{
						object_code: "KH001",
						object_name: "Tên đối tượng",
						tax_code: "Mã số thuế",
						address: "Địa chỉ",
						phone_number: "Số điện thoại",
					},
				],
			};
		},
	};
</script>
<style>
	@import url("../../../css/base/select/listgrid.css");
</style>
