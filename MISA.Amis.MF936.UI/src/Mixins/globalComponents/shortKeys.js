export default {
    data() {
        return {
            keyPressTimeout: null
        }
    },
    methods: {
        /**
         * Gán phím tắt vào component
         */
        bindShortKeys() {
            /**
			 * Bắt sự kiện keydown
			 */

            console.log('binded short key');
			document.addEventListener("keydown", (event)  => {this.bind(event)});
        },
        unBindShortKeys() {
            document.removeEventListener('keydown', this.bind(), true);
        },
        bind(event) {
            switch (event.key) {
                case "Escape":
                    event.preventDefault();
                    this.shortKeyDelay(0);
                    console.log('escape')
                    break;
                case "F1":
                    event.preventDefault();
                    this.shortKeyDelay(0);
                    console.log('help')
                    break;
                case "Control":
                    event.preventDefault();
                    this.keyPresses["Control"] = true;
                    this.shortKeyDelay(500);
                    break;
                case "Shift":
                    event.preventDefault();
                    if (this.keyPresses["Control"]) {
                        this.keyPresses["Shift"] = true;
                        this.shortKeyDelay(500);
                    } else this.shortKeyDelay(0);
                    break;
                default:
                    if (event.key == "S") {
                        if (this.keyPresses["Control"]) {
                            event.preventDefault();
                            console.log('ctrl shift s')
                        }
                        this.shortKeyDelay(0);
                    }
                    if (event.key == "s") {
                        if (this.keyPresses["Control"]) {
                            event.preventDefault();
                            console.log('ctrl s')
                        }
                        this.shortKeyDelay(0);
                    }
                    break;
            }     
        },
        /**
         * Hàm xử lý delay phím tắt
         * CreatedBy: NTDUNG (03/09/2021)
         */
        shortKeyDelay(duration) {
            clearTimeout(this.keyPressTimeout);
            this.keyPressTimeout = setTimeout(() => {
                this.keyPresses = [];
            }, duration);
        },
    }
}