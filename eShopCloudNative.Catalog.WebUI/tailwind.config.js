const colors = require('tailwindcss/colors')
const defaultTheme = require('tailwindcss/defaultTheme')

module.exports = {
    content: [
        "./Views/**/*.cshtml",        
    ],
    safelist: [
        // {
        //     pattern: /.+/s
        // },
        //{
        //    pattern: /(bg|text|shadow)-(slate|gray|zinc|neutral|stone|red|orange|amber|yellow|lime|green|emerald|teal|cyan|sky|blue|indigo|violet|purple|fuchsia|pink|rose)-(50|100|200|300|400|500|600|700|800|900)/,
        //    variants: ['sm', 'md', 'lg', 'xl', '2xl', '3xl'],
        //},
        //{
        //    pattern: /(bg|text|shadow)-(white|black|inherit|current|transparent|left|center|right|justify)/,
        //    variants: ['sm', 'md', 'lg', 'xl', '2xl', '3xl'],
        //},
        //{
        //    pattern: /(aspect)-(w|h)-(1|2|3|4|5|6|7|8|9|10|11|12|13|14|15|16)/,
        //    variants: ['sm', 'md', 'lg', 'xl', '2xl', '3xl'],
        //},
        /*{
            pattern: /(aspect)-(ratio|video)/,
            variants: ['sm', 'md', 'lg', 'xl', '2xl', '3xl', 'hover', 'focus', 'lg:hover'],
        },*/
        // {
        //     pattern: /(p|px|py|pt|pr|pb|pl)-(px|0|0.5|1|1.5|2|2.5|3|3.5|4|5|6|7|8|9|10|11|12|14|16|20|24|28|32|36|40|44|48|52|56|60|64|72|80|96)/,
        //     variants: ['sm', 'md', 'lg', 'xl', '2xl', '3xl', 'hover', 'focus', 'lg:hover'],
        // },
        // {
        //     pattern: /(m|mx|my|mt|mr|mb|ml)-(px|0|0.5|1|1.5|2|2.5|3|3.5|4|5|6|7|8|9|10|11|12|14|16|20|24|28|32|36|40|44|48|52|56|60|64|72|80|96)/,
        //     variants: ['sm', 'md', 'lg', 'xl', '2xl', '3xl', 'hover', 'focus', 'lg:hover'],
        // },
        // {
        //     pattern: /(w)-(px|0|0.5|1|1.5|2|2.5|3|3.5|4|5|6|7|8|9|10|11|12|14|16|20|24|28|32|36|40|44|48|52|56|60|64|72|80|96|auto|full|screen|min|max|fit)/,
        //     variants: ['sm', 'md', 'lg', 'xl', '2xl', '3xl', 'hover', 'focus', 'lg:hover'],
        // }
    ],
    presets: [],
    darkMode: 'media', // 'media' or 'class'
    theme: {
        extend: {
            screens: {
                sm: '425px',
                md: '768px',
                lg: '1024px',
                xl: '1280px',
                '2xl': '1536px',
                '3xl': '1600px'
            },
            fontFamily: {
                sans: [
                    'Roboto',
                    ...defaultTheme.fontFamily.sans,
                ],
                serif: [
                    ...defaultTheme.fontFamily.serif,
                ],
                mono: [
                    ...defaultTheme.fontFamily.mono,
                ],
            },
            fontSize: {
                xs: ['0.75rem', { lineHeight: '1rem' }],
                sm: ['0.875rem', { lineHeight: '1.25rem' }],
                base: ['1rem', { lineHeight: '1.5rem' }],
                lg: ['1.125rem', { lineHeight: '1.75rem' }],
                xl: ['1.25rem', { lineHeight: '1.75rem' }],
                '2xl': ['1.5rem', { lineHeight: '2rem' }],
                '3xl': ['1.875rem', { lineHeight: '2.25rem' }],
                '4xl': ['2.25rem', { lineHeight: '2.5rem' }],
                '5xl': ['3rem', { lineHeight: '1' }],
                '6xl': ['3.75rem', { lineHeight: '1' }],
                '7xl': ['4.5rem', { lineHeight: '1' }],
                '8xl': ['6rem', { lineHeight: '1' }],
                '9xl': ['8rem', { lineHeight: '1' }],
            },
            fontWeight: {
                thin: '100',
                extralight: '200',
                light: '300',
                normal: '400',
                medium: '500',
                semibold: '600',
                bold: '700',
                extrabold: '800',
                black: '900',
            },
            aspectRatio: {
                1: '1',
                2: '2',
                3: '3',
                4: '4',
                5: '5',
                6: '6',
                7: '7',
                8: '8',
                9: '9',
                10: '10',
                11: '11',
                12: '12',
                13: '13',
                14: '14',
                15: '15',
                16: '16',
            },
            variants: {
                aspectRatio: ['responsive', 'hover']
            }
        },
        ...defaultTheme
    },
    corePlugins: {
        aspectRatio: false,
    },
    variantOrder: [
        'first',
        'last',
        'odd',
        'even',
        'visited',
        'checked',
        'empty',
        'read-only',
        'group-hover',
        'group-focus',
        'focus-within',
        'hover',
        'focus',
        'focus-visible',
        'active',
        'disabled',
    ],
    plugins: [
        require('@tailwindcss/typography'),
        require('@tailwindcss/forms'),
        require('@tailwindcss/line-clamp'),
        require('@tailwindcss/aspect-ratio'),
        require('daisyui'),
        require('tw-elements/dist/plugin')
    ],
}
