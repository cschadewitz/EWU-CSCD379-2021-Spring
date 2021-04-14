const colors = require('tailwindcss/colors')

module.exports = {
    purge: ['./**/*.html', './**/*.cshtml'],
    theme: {
        colors: {
            transparent: 'transparent',
            current: 'currentColor',
            gray: colors.warmGray,
            red: colors.red,
            green: colors.green,
            yellow: colors.yellow,
        },
        extend: {}
    },
    variants: {
    },
    plugins: [
        require('@tailwindcss/forms'),
    ]
}