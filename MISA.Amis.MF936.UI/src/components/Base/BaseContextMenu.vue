<template lang="">
	<div
		class="context-menu-container"
		:class="{ 'selectbox--show': contextMenuState, 'selectbox--up': isUp }"
		:style="positionOfContextMenu"
	>
		<ul ref="contextMenu" class="selectbox__list">
			<li
				v-for="(item, index) in contextFunction"
				@click="choose(index)"
				class="selectbox__item"
				:key="index"
			>
				{{ item["name"] }}
			</li>
		</ul>
	</div>
</template>
<script>
	export default {
		name: "BaseContextMenu",
		data() {
			return {
				contextMenuState: false,
				contextFunction: [],
				isUp: false,
				top: -100,
				left: -100,
                width: 0,
                height: 0,
			};
		},	
        computed: {
            /**
             * Tính toán vị trí aủa contextmenu
             * CreatedBy: NTDUNG (23/09/2021)
             */
            positionOfContextMenu() {
                if (this.isUp) {
                    return {
                        top: this.top + 'px',
                        left: this.left + this.width + 'px'
                    }
                } else {
                    return {
                        top: this.top + this.height + 'px',
                        left: this.left + this.width + 'px'
                    }
                }
            }
        },
		created() {
			/**
			 * Tạo event hiển thị context menu
			 * CreatedBy: NTDUNG (20/09/2021)
			 */
			this.$bus.$on("showContextMenu", (data) => {
				this.contextMenuState = true;
				this.contextFunction = data["contextFunction"];

                this.top = data['pos']['top'];
                this.left = data['pos']['left'];
                this.width = data['pos']['width'];
                this.height = data['pos']['height'];

                this.$nextTick(() => {
                    this.setPositionOfContextMenu();
                });
			});
			/**
			 * Tạo event ẩn context menu
			 * CreatedBy: NTDUNG (20/09/2021)
			 */
			this.$bus.$on("hideContextMenu", () => {
				setTimeout(() => {	
					this.contextMenuState = false;
				}, 100);
			});
		},
		methods: {
            /**
             * Đặt vị trí cho contextmenu
             * CreatedBy: NTDUNG (23/09/2021)
             */
            setPositionOfContextMenu() {
                if (this.$refs.contextMenu) {
                    var bottom = this.$refs.contextMenu.getBoundingClientRect().bottom;
                    if (bottom + 46 >= window.innerHeight) {
                        this.isUp = true;
                    } else {
                        this.isUp = false;
                    }
                } 
			},
			/**
			 * Chọn chức năng trên context menu
			 * @param {Number} index
			 * CreatedBy: NTDUNG (20/09/2021)
			 */
			choose(index) {
				this.contextFunction[index]['function']();
				this.contextMenuState = false;
			},
		},
		destroyed() {
			/**
			 * Huỷ các sự kiện
			 * CreatedBy: NTDUNG (20/09/2021)
			 */
			this.$bus.$off("showContextMenu");
			this.$bus.$off("hideContextMenu");
		},
	};
</script>
<style>
	@import url("../../css/base/contextmenu.css");
</style>
