<template lang="">
	<div v-if="false" class="selectcommodity__overlay">
		<div
			class="selectcommodity"
			:class="{ 'selectcommodity--fullscreen': fullscreen }"
		>
			<div class="selectcommodity__header fx-space-between">
				<div class="selectcommodity__title">
					Chọn tính chất hàng hoá dịch vụ
				</div>
				<div class="selectcommodity__close">
					<base-help />
					<base-cancel :method="hideForm"/>
				</div>
			</div>
			<ul class="selectcommodity__list">
				<li
					v-for="(item, index) in data"
					class="selectcommodity__item"
					:key="index"
				>
					<div
						class="selectcommodity__item-icon"
						:style="{ 'background-position': item['position'] }"
					></div>
					<div class="selectcommodity__item-content">
						<div class="selectcommodity__item-title">{{ item["title"] }}</div>
						<div class="selectcommodity__item-desc">{{ item["desc"] }}</div>
					</div>
				</li>
			</ul>
			<div
				@click="fullscreen = !fullscreen"
				class="selectcommodity__extend"
                :style="{'transform': fullscreen ? 'rotate(180deg)' : 'rotate(0)'}"
			></div>
		</div>
	</div>
</template>
<script>
	// COMPONENTS
	import BaseHelp from "../../../Base/Form/components/BaseHelp.vue";
	import BaseCancel from "../../../Base/Form/components/BaseCancel.vue";

	export default {
		name: "WarehouseSelectCommodity",
		components: {
			BaseHelp,
			BaseCancel,
		},
		data() {
			return {
				fullscreen: false,
				data: [
					{
						position: "-32px -632px",
						title: "Hàng hoá",
						desc: "Sản phẩm bạn mua và bán lại cho khách hàng",
					},
					{
						position: "-264px -632px",
						title: "Dịch vụ",
						desc: "Dịch vụ mà bạn cung cấp cho khách hàng",
					},
					{
						position: "-960px -632px",
						title: "Nguyên vật liệu",
						desc:
							"Nguyên liệu đầu vào dùng cho hoạt động sản xuất, xây dựng, cung cấp dịch vụ",
					},
					{
						position: "-148px -632px",
						title: "Thành phẩm",
						desc: "Là sản phẩm đầu ra của quá trình sản xuất",
					},
					{
						position: "-1079px -632px",
						title: "Công cụ dụng cụ",
						desc: "Công cụ dụng cụ mua về nhập kho chưa đưa vào sử dụng",
					},
				],
			};
		},
        methods: {
            hideForm() {
                this.$emit('input', false);
            }
        }
	};
</script>
<style>
	@import url("../../../../css/views/warehouse/warehouseselectcommodity.css");
</style>
