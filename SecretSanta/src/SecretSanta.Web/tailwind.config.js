module.exports = {
    purge: ['./**/*.html', './**/*.cshtml'],
    theme: {
        colors: {
            gray: colors.coolGray,
            blue: colors.lightBlue,
        }
        extend: {}
    },
    variants: {
    },
    plugins: [
        require('@tailwindcss/forms'),
        require('@tailwindcss/aspect-ratio'),
        require('@tailwindcss/typography'),
        require('tailwindcss-children'),
    ]
}